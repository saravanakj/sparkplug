global using System;
global using System.Linq;
global using System.Reflection;
global using System.Net;
global using System.Transactions;
global using Microsoft.AspNetCore;
global using Microsoft.Extensions.Logging;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Hosting;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.ApplicationParts;
global using Microsoft.AspNetCore.Mvc.Controllers;
global using Microsoft.AspNetCore.Mvc.ApplicationModels;
global using System.Globalization;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.AspNetCore.Http;
global using System.Text.Json;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using Microsoft.OpenApi.Models;
global using Microsoft.Identity.Web;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc.Core;
global using Microsoft.AspNetCore.Mvc.ApiExplorer;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Mvc.Filters;
global using System.ComponentModel.DataAnnotations;

global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using Microsoft.IdentityModel.Tokens;

global using SparkPlug.Contracts;
// global using SparkPlug.Api.Abstractions;
global using SparkPlug.Persistence.Abstractions;

global using SparkPlug.Api;
global using SparkPlug.Api.Controllers;
global using SparkPlug.Api.Configuration;
global using SparkPlug.Api.Model;
global using SparkPlug.Api.Middleware;
global using SparkPlug.Api.Abstractions.Tenant;
global using SparkPlug.Api.Filters;
