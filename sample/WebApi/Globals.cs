global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.AspNetCore.Mvc.ApplicationParts;
global using Microsoft.AspNetCore.Mvc;
global using MongoDB.Bson;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.JsonPatch;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.Identity.Web;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Options;

global using SparkPlug.Api;
global using SparkPlug.Api.Controllers;
global using SparkPlug.Api.Configuration;
global using SparkPlug.Contracts;

global using SparkPlug.Persistence.Abstractions;
global using SparkPlug.Persistence.MongoDb;
global using SparkPlug.Persistence.MongoDb.Models;
global using SparkPlug.Persistence.MongoDb.Attributes;
global using SparkPlug.Persistence.MongoDb.Context;
global using SparkPlug.Persistence.MongoDb.Configuration;

global using SparkPlug.Sample.WebApi.Models;
global using SparkPlug.Sample.WebApi.Repositories;
