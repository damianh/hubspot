# Priority 5: Additional CRM APIs Implementation Plan

## Overview
The CRM category has many more APIs beyond standard objects that need implementation.

## CRM APIs to Implement

### High Priority (Commonly Used)
1. **Associations** (`/crm/v3/associations` & `/crm/v4/associations`)
   - Associate CRM objects (e.g., Contact to Company)
   - Critical for real-world scenarios

2. **Properties** (`/crm/v3/properties`)
   - Manage custom properties for objects
   - Essential for customization

3. **Pipelines** (`/crm/v3/pipelines`)
   - Deal/Ticket pipelines and stages
   - Commonly used for sales workflows

4. **Owners** (`/crm/v3/owners`)
   - User/team ownership of records
   - Important for assignment workflows

5. **Lists** (`/crm/v3/lists`)
   - Contact/Company lists
   - Used for segmentation

### Medium Priority (Business Features)
6. **Imports** (`/crm/v3/imports`)
   - Bulk data imports
   - Testing import workflows

7. **Exports** (`/crm/v3/exports`)
   - Bulk data exports
   - Testing export functionality

8. **Custom Objects** (`/crm/v3/objects/custom`)
   - User-defined object types
   - Advanced customization

9. **Schemas** (`/crm/v3/schemas`)
   - Object schema definitions
   - Required for custom objects

10. **Timeline** (`/crm/v3/timeline`)
    - Activity timeline events
    - Record history tracking

### Commerce Objects (If Needed)
11. **Carts** (`/crm/v3/objects/carts`)
12. **Orders** (`/crm/v3/objects/orders`)
13. **Invoices** (`/crm/v3/objects/invoices`)
14. **Discounts** (`/crm/v3/objects/discounts`)
15. **Fees** (`/crm/v3/objects/fees`)
16. **Taxes** (`/crm/v3/objects/taxes`)
17. **Commerce Payments** (`/crm/v3/objects/commerce_payments`)
18. **Commerce Subscriptions** (`/crm/v3/objects/commerce_subscriptions`)

### Specialized (Lower Priority)
19. **Leads** (`/crm/v3/objects/leads`)
20. **Listings** (`/crm/v3/objects/listings`)
21. **Contracts** (`/crm/v3/objects/contracts`)
22. **Courses** (`/crm/v3/objects/courses`)
23. **Services** (`/crm/v3/objects/services`)
24. **Deal Splits** (`/crm/v3/objects/deal_splits`)
25. **Goal Targets** (`/crm/v3/objects/goal_targets`)
26. **Partner Clients** (`/crm/v3/objects/partner_clients`)
27. **Partner Services** (`/crm/v3/objects/partner_services`)

### Extensions & Integrations
28. **Calling Extensions** (`/crm/v3/extensions/calling`)
29. **Video Conferencing Extension** (`/crm/v3/extensions/videoconferencing`)
30. **Public App CRM Cards** (`/crm/v3/extensions/cards`)

### Other CRM Features
31. **Appointments** (`/crm/v3/objects/appointments`)
32. **Transcriptions** (`/crm/v3/objects/transcriptions`)
33. **Object Library** (`/crm/v3/object-library`)
34. **Property Validations** (`/crm/v3/property-validations`)
35. **Public App Feature Flags** (`/crm/v3/feature-flags`)
36. **Limits Tracking** (`/crm/v3/limits`)

## Implementation Status
- ❌ None implemented yet

## Next Steps
1. Start with **Associations** - critical for linking objects
2. Then **Properties** - needed for custom fields
3. Then **Pipelines** - common sales workflow feature
4. Then **Owners** - assignment functionality
5. Then **Lists** - segmentation feature
