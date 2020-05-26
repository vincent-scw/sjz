# sjz
**sjz** stands for 时间轴. It is a practice project for microservices architecture using ASP.NET Core, Angular and Docker.

## Functional Services

**sjz** was decomposed into 3 microservices. All of them are independently deployable applications.

<img width="660" alt="Architecture" src="https://github.com/vincent-scw/sjz/blob/master/doc/architecture.png" />


#### User Profile Service
Contains general user information: user account, liked timeline, following users.

Data Storage: neo4j

Method | Path | Description | User authenticated 
------------- | ------------------------- | ------------- |:-------------:|


#### Timeline Service
Perform actions on Timelines.

Data Storage: Mongodb

Method	| Path	| Description	| User authenticated	
------------- | ------------------------- | ------------- |:-------------:|
GET     | /timelines | Query timelines | ×
GET     | /timelines/{id} | Get a specific timeline | ×
POST    | /timelines | Insert/Update a specific timeline |
DELETE  | /timelines/{id} | Delete a specific timeline |
POST    | /timelines/{id}/records | Insert/Update a specific record in timeline |
DELETE  | /timelines/{id}/records/{recordId} | Delete a timeline record |

#### Image Service
Upload or list images to Azure Blob Storage.

Data Storage: Azure Blob Storage

Method | Path | Description | User authenticated 
------------- | ------------------------- | ------------- |:-------------:|
GET    | /images | List images | ×
POST/PUT | /images/upload | Upload image | 

#### Notes
- Each microservice has its own database (Azure Blob storage for Image Service).
- In different service, I use different database to meet the scenarios. 
For example, graph database **neo4j** is used in User Profile Service, because graph database is more suitable to handle user-to-user relationships. 

## Infrastructure services

#### Auth Service
The project leverages [IdentityServer4](https://github.com/IdentityServer/IdentityServer4) and provides OAuth2 and OpenID authentication.
To make life simple, I only allow External login (Github & Linkedin). I use [`Authorization Code with PKCE`](https://tools.ietf.org/html/rfc6749#section-1.3.1) grant type for users authorization,
and [`Client Credentials`](https://tools.ietf.org/html/rfc6749#section-4.4) grant for microservices authorization.

### Get started locally
- Use docker-compose: `docker-compose up`
- Visit http://localhost:4200
  
