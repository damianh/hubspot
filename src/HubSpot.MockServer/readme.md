# HubSpot Mock Server

An easy to use mock server for that implements a subset of HubSpot APIs
for use in testing with the HubSpot.Client library.

## Implemented APIs

### CRM

#### Companies

Batch:

- [ ] Archive a batch of companies by ID `POST /crm/v3/objects/companies/batch/archive`
- [ ] Create a batch of companies `POST /crm/v3/objects/companies/batch/create`
- [ ] Read a batch of companies by ID `POST /crm/v3/objects/companies/batch/read`
- [ ] Update a batch of companies `POST /crm/v3/objects/companies/batch/update`

Basic:

- [x] List `GET /crm/v3/objects/companies`
- [x] Read `GET /crm/v3/objects/companies/{companyId}`
- [x] Create `POST /crm/v3/objects/companies`
- [x] Update `PATCH /crm/v3/objects/companies/{companyId}`
- [x] Archive `DELETE /crm/v3/objects/companies/{companyId}`

Public_Object:

- [ ] Merge `POST /crm/v3/objects/companies/merge`

Search:

- [ ] Search `POST /crm/v3/objects/companies/search`
