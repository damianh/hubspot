# Priority 6: Non-CRM APIs Implementation Plan

## Overview
Implement the major API categories beyond CRM.

## API Categories to Implement

### 1. Files API
**Path:** `/files/v3`
**Purpose:** File upload, storage, and management
**Priority:** High (commonly needed for attachments)
**Endpoints:**
- Upload files
- List files
- Get file metadata
- Delete files
- Generate signed URLs

### 2. CMS APIs
**Purpose:** Content Management System
**Priority:** Medium-High (if testing CMS integrations)
**Sub-APIs:**
- Content pages
- Blog posts
- Site pages
- Templates
- Modules
- Themes

### 3. Conversations APIs
**Purpose:** Live chat, messaging, and communication
**Priority:** Medium
**Sub-APIs:**
- **Inbox & Messages** (`/conversations/v3/conversations`)
  - Thread management
  - Message sending/receiving
- **Custom Channels** (`/conversations/v3/channels`)
  - Integration channels
- **Visitor Identification** (`/conversations/v3/visitor-identification`)
  - Identify website visitors

### 4. Automation APIs
**Purpose:** Workflow automation
**Priority:** Medium-High (if testing workflows)
**Typical Features:**
- Workflow execution
- Workflow management
- Custom actions

### 5. Events API
**Path:** `/events/v3`
**Purpose:** Custom behavioral events
**Priority:** Medium
**Note:** Different from Marketing Events (already implemented)

### 6. Scheduler API
**Path:** `/scheduler/v3`
**Purpose:** Meeting scheduling
**Priority:** Low-Medium
**Features:**
- Meeting links
- Availability
- Booking pages

### 7. Settings APIs
**Purpose:** Account and app settings
**Priority:** Low-Medium
**Features:**
- User settings
- Account configuration
- App preferences

### 8. Communication Preferences
**Path:** `/communication-preferences/v3`
**Purpose:** Subscription management
**Priority:** Medium
**Features:**
- Email subscriptions
- Communication preferences
- Opt-in/opt-out management

### 9. Business Units
**Path:** `/business-units/v3`
**Purpose:** Multi-business unit management
**Priority:** Low (enterprise feature)

### 10. Auth APIs
**Path:** `/auth/v1` or `/oauth/v1`
**Purpose:** Authentication and authorization
**Priority:** Low for mock server (usually handled differently)
**Note:** May not need full implementation

### 11. Account APIs
**Purpose:** Account information and management
**Priority:** Low for mock server

## Recommended Implementation Order

### Phase 1: Essential Non-CRM (Priority 6.1)
1. ✅ **Files API** - Commonly needed, straightforward to implement
2. **Events API** - Custom event tracking, complements Marketing Events

### Phase 2: Communication & Engagement (Priority 6.2)
3. **Communication Preferences** - Important for email/subscription testing
4. **Conversations API** - If testing chat/messaging features

### Phase 3: Content & Automation (Priority 6.3)
5. **Automation API** - If testing workflow triggers
6. **CMS APIs** - If testing content management (large scope)

### Phase 4: Specialized (Priority 6.4)
7. **Scheduler API** - If testing meeting booking
8. **Settings APIs** - If testing configuration
9. **Business Units** - Enterprise feature
10. **Account APIs** - Low priority
11. **Auth APIs** - Usually tested differently

## Implementation Status
- ❌ None implemented yet (except Marketing Events and Webhooks from earlier priorities)

## Next Steps After Priority 5
- Implement Files API first (most universally useful)
- Then Events API
- Assess need for others based on actual testing requirements
