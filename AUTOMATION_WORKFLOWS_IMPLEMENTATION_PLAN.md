# Automation & Workflows API Implementation Plan

## Overview
Implementation plan for HubSpot Automation and Workflows APIs in the MockServer. These APIs handle custom automation actions and sequence enrollment workflows.

## Current Status
- **Generated Clients**: ✅ Available
  - `HubSpotAutomationActionsV4V4Client` 
  - `HubSpotAutomationSequencesV4Client`
- **Mock Server Implementation**: ❌ Not started
- **Tests**: ❌ Not created

## API Analysis

### 1. Automation Actions API (v4)
**Base Path**: `/automation/v4/actions`

#### Endpoints:
1. **POST /automation/v4/actions/callbacks/complete**
   - Complete a batch of blocked action executions
   - Request: `BatchInputCallbackCompletionBatchRequest`
   - Response: No content (204)

2. **GET /automation/v4/actions/{appId}**
   - Get custom automation actions for an app
   - Path param: `appId` (int)
   - Response: List of actions

3. **Callbacks Management**
   - Complete callbacks in batch

**Purpose**: 
- Allows apps to define custom automation actions
- Manage callback completions for blocked workflows

### 2. Automation Sequences API (v4)
**Base Path**: `/automation/v4/sequences`

#### Endpoints:
1. **POST /automation/v4/sequences/enrollments**
   - Enroll a contact into a sequence
   - Request: `PublicSequenceEnrollmentRequest`
   - Response: `PublicSequenceEnrollmentLiteResponse`

2. **POST /automation/v4/sequences/enrollments/contact**
   - Contact-specific enrollment operations
   - Sub-endpoints for contact enrollment management

3. **GET /automation/v4/sequences/{sequenceId}**
   - Get sequence details
   - Path param: `sequenceId` (string)

4. **GET /automation/v4/sequences** (EmptyPathSegment)
   - List sequences

**Purpose**:
- Manage sales sequences (automated email/task workflows)
- Enroll contacts into sequences
- Track enrollment status

## Implementation Strategy

### Phase 1: Data Models & Repository
**Priority**: High
**Complexity**: Medium

1. **Create Automation Models**
   ```csharp
   // src/HubSpot.MockServer/Apis/Models/AutomationModels.cs
   - CustomAction
   - CallbackCompletion
   - Sequence
   - SequenceEnrollment
   - SequenceStep
   ```

2. **Create Repositories**
   ```csharp
   // src/HubSpot.MockServer/Repositories/AutomationRepository.cs
   - Store custom actions by appId
   - Manage callback completions
   
   // src/HubSpot.MockServer/Repositories/SequenceRepository.cs
   - Store sequences
   - Track enrollments
   - Manage enrollment states (ENROLLED, PAUSED, COMPLETED, etc.)
   ```

3. **Register Repositories**
   - Add to `HubSpotMockServer.cs` DI container

### Phase 2: Mock Server Implementation
**Priority**: High  
**Complexity**: Medium

1. **Create API Routes File**
   ```csharp
   // src/HubSpot.MockServer/ApiRoutes.Automation.cs
   internal static partial class ApiRoutes
   {
       internal static class Automation
       {
           public static void RegisterAutomationActionsV4(WebApplication app);
           public static void RegisterAutomationSequencesV4(WebApplication app);
       }
   }
   ```

2. **Implement Actions Endpoints**
   - POST /automation/v4/actions/callbacks/complete
   - GET /automation/v4/actions/{appId}
   - Use MapGroup for `/automation/v4/actions`

3. **Implement Sequences Endpoints**
   - POST /automation/v4/sequences/enrollments
   - POST /automation/v4/sequences/enrollments/contact
   - GET /automation/v4/sequences/{sequenceId}
   - GET /automation/v4/sequences
   - Use MapGroup for `/automation/v4/sequences`

4. **Register in HubSpotMockServer**
   ```csharp
   ApiRoutes.Automation.RegisterAutomationActionsV4(app);
   ApiRoutes.Automation.RegisterAutomationSequencesV4(app);
   ```

### Phase 3: Client Extensions (Optional)
**Priority**: Low
**Complexity**: Low

1. **Create Helper Extensions**
   ```csharp
   // src/HubSpot.KiotaClient/Extensions/AutomationExtensions.cs
   public static class HubSpotAutomationExtensions
   {
       // Convenience methods if needed
   }
   ```

### Phase 4: Integration Tests
**Priority**: High
**Complexity**: Medium

1. **Create Test File**
   ```csharp
   // test/HubSpot.Tests/Automation/AutomationActionsTests.cs
   - Test callback completion
   - Test action retrieval by appId
   
   // test/HubSpot.Tests/Automation/SequencesTests.cs
   - Test sequence creation
   - Test contact enrollment
   - Test enrollment state transitions
   - Test sequence retrieval
   ```

2. **Test Scenarios**
   - Enroll contact in sequence
   - Verify enrollment response
   - Complete action callbacks
   - List sequences
   - Get sequence by ID

## Implementation Priority

### ⚠️ Complexity Assessment
- **Low Complexity**: These APIs are simpler than CRM objects
  - No associations required
  - No complex property schemas
  - Focused domain (workflows/automation)
  
### Priority Ranking
1. **P1 - Sequences API**: More commonly used for sales workflows
2. **P2 - Actions API**: Used by custom app integrations

## Data Storage Requirements

### AutomationRepository
```csharp
- Dictionary<int, List<CustomAction>> _actionsByApp
- Dictionary<string, CallbackCompletion> _callbacks
```

### SequenceRepository
```csharp
- Dictionary<string, Sequence> _sequences
- Dictionary<string, SequenceEnrollment> _enrollments (by enrollmentId)
- Dictionary<string, List<SequenceEnrollment>> _enrollmentsByContact
- Dictionary<string, List<SequenceEnrollment>> _enrollmentsBySequence
```

## Testing Approach

### Unit Test Coverage
1. **Actions API**
   - Complete callbacks (batch)
   - Retrieve actions by app ID
   - Handle missing app IDs

2. **Sequences API**
   - Enroll contact (success)
   - Enroll already-enrolled contact (idempotency)
   - Get sequence details
   - List sequences
   - Enrollment state management

### Integration Test Pattern
```csharp
[Fact]
public async Task EnrollContactInSequence_ReturnsEnrollmentDetails()
{
    await using var mockServer = await HubSpotMockServer.StartAsync();
    var client = new HubSpotAutomationSequencesV4Client(mockServer.CreateRequestAdapter());
    
    var enrollment = await client.Automation.V4.Sequences.Enrollments.PostAsync(
        new PublicSequenceEnrollmentRequest
        {
            ContactId = "12345",
            SequenceId = "seq-1",
            UserId = 100
        });
    
    enrollment.Should().NotBeNull();
    enrollment.EnrollmentId.Should().NotBeNullOrEmpty();
}
```

## Model Examples

### Sequence Model
```csharp
public record Sequence
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public List<SequenceStep> Steps { get; init; } = new();
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}

public record SequenceStep
{
    public int StepNumber { get; init; }
    public string Type { get; init; } = ""; // EMAIL, TASK, etc.
    public int DelayMinutes { get; init; }
}

public record SequenceEnrollment
{
    public string EnrollmentId { get; init; } = string.Empty;
    public string ContactId { get; init; } = string.Empty;
    public string SequenceId { get; init; } = string.Empty;
    public int UserId { get; init; }
    public string State { get; init; } = "ENROLLED"; // ENROLLED, PAUSED, COMPLETED, etc.
    public DateTimeOffset EnrolledAt { get; init; }
}
```

### Custom Action Model
```csharp
public record CustomAction
{
    public string Id { get; init; } = string.Empty;
    public int AppId { get; init; }
    public string Label { get; init; } = string.Empty;
    public string ActionUrl { get; init; } = string.Empty;
    public List<ActionInput> InputFields { get; init; } = new();
}

public record CallbackCompletion
{
    public string CallbackId { get; init; } = string.Empty;
    public string Status { get; init; } = ""; // SUCCESS, FAILURE
    public DateTimeOffset CompletedAt { get; init; }
}
```

## Dependencies
- ✅ `HubSpotMockServer` infrastructure (exists)
- ✅ MapGroup routing pattern (established)
- ✅ Repository pattern (established)
- ✅ Generated Kiota clients (available)

## Estimated Effort
- **Phase 1 (Models & Repositories)**: 2-3 hours
- **Phase 2 (API Routes)**: 3-4 hours
- **Phase 3 (Extensions)**: 1 hour (optional)
- **Phase 4 (Tests)**: 2-3 hours
- **Total**: 8-11 hours

## Implementation Order

### Step 1: Sequences API (Higher Priority)
1. Create `SequenceRepository`
2. Create `ApiRoutes.Automation.cs` with sequences endpoints
3. Register in `HubSpotMockServer.cs`
4. Create `SequencesTests.cs`
5. Verify all tests pass

### Step 2: Actions API  
1. Create `AutomationRepository`
2. Add actions endpoints to `ApiRoutes.Automation.cs`
3. Register in `HubSpotMockServer.cs`
4. Create `AutomationActionsTests.cs`
5. Verify all tests pass

## Success Criteria
- ✅ All automation/sequence endpoints implemented
- ✅ Repositories handle state correctly
- ✅ Tests cover happy path and edge cases
- ✅ Both generated clients work against mock server
- ✅ No compilation errors
- ✅ All tests pass

## Notes
- These APIs are less complex than full CRM objects
- No association handling needed
- No property schema management required
- Focus on enrollment state management for sequences
- Actions API primarily for callback completions

## Post-Implementation
After completion, update:
- `FINAL_IMPLEMENTATION_STATUS.md`
- Add to implemented APIs list
- Update overall completion percentage
