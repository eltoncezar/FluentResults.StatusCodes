# FluentResults.StatusCodes
Simple status code management using FluentResults errors as validation.

# Getting Started
First add  to your project the latest Nuget package from the OpenMind feed.

Then, create your own errors inheriting from one of the base errors:
```csharp
InvalidInputError        //will produce a 400
UnauthorizedError        //will produce a 403
NotFoundError            //will produce a 404
BusinessValidationError  //will produce a 409
```
Their constructors are simple strings, so you are encouraged to create your own constructors with custom logic.

```csharp
public class MyCustomError : BusinessValidationError
{
    public MyCustomError(string message, int value, object otherValue) : base($"{message} {value} {otherValue.ToString()}")
    {
    }
}
```

Those errors are automatically handled when you call the `ValidateResult` method in your controllers, and the right error code is returned.
You can either inherit from the `ResultControllerBase` or call the static method on the Result object.

1. First option, inheriting controller
   ```csharp
   public class YourController : ResultControllerBase
   {
       [HttpGet]
       public IActionResult Get()
       {
           var result = Result.Ok(); //call your logic here
           return ValidateResult(result, 
               () => Ok(),   //on success 
               () => {}      //optional on failure
           ); 
       }
   }
   ```
1. Second option, no controller inheritance
   ```csharp
   public class YourController : ControllerBase
   {
       [HttpGet]
       public IActionResult Get()
       {
           var result = Result.Ok();
           return result.Validate(() => Ok()); 
       }
   }
   ```

# Build and Test
Just clone and build. There are no tests at the moment. Feel free to create them :)

# Contribute
This is an OpenMind public project. You are free to do whatever you want with it, just be cautious to not break any existing funcionality.

# How to publish a new version

1. Increment the `<Version>` in the `.csproj` file
1. Pack the new version (should be done automatically in build) and note the path.
1. Push the new package

   ```dotnet nuget push --source "OpenMind" --api-key az "%PATH_TO_NUPKG%"```

# How to consume the package

Follow the steps described here: [https://dev.azure.com/sda-iatec/OpenMind.Arquitetura/_packaging?_a=connect&feed=OpenMind](https://dev.azure.com/sda-iatec/OpenMind.Arquitetura/_packaging?_a=connect&feed=OpenMind)