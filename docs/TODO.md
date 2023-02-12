# Todo

1. Serializer Implmentation for ApiRequests to send as payload or querystring in SparkPlug.Contract library. 
    a. .ToJson()
    b. .ToBase64()

2. RESTSharp integration to get or post data to REST API Url in SparkPlug.Contract library. 
    a. .Get("/user/10")
    b. .Post("/user")
    c. .Put("/user")
    d. .Delete("/user/10")
    e. .Patch("/user/10")

3. SparkPlug.Api Unit Testing with MongoDb
5. GenericTypeControllerFeatureProvider - Add controller to feature.Controllers with sapcific api verbs (only GET and POST like).
6. Dynamically add api version in Swagger docs.
8. Generick Cache interface need to be implement for SparkPlug.Api
9. Update config if the configuration is modifyed in the cloud.
10. Functional Module list
    a. RBAC based Authorization, UI Screens mappings for role
    b. Multi-Tenancy Management
    c. 
11. Distributed Cache interface implementation to cache data including Tenant information.

# Done

4. All Rest API make it workable with MongoDb sample Application. - Done
7. Transactions handling - Done