using Microsoft.AspNetCore.Mvc;
using NHibernate;

namespace Turnit.GenericStore.Api.Features;

[ApiController]
[Produces("application/json")]
public abstract class ApiControllerBase : ControllerBase
{
	
}