﻿global using System;
global using System.Linq;
global using System.Reflection;
global using System.Collections.Concurrent;
global using System.Security.Authentication;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.IO;
global using System.Net;
global using System.Threading;
global using System.Runtime.Serialization;
global using System.Text;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Routing;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.Extensions.Hosting;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Controllers;
global using Microsoft.AspNetCore.Mvc.ApplicationModels;
global using System.Text.Json;
global using Microsoft.Extensions.Options;

global using MongoDB.Driver;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;

global using SparkPlug.Contracts;
global using SparkPlug.Persistence.Abstractions;

global using SparkPlug.Persistence.MongoDb;
global using SparkPlug.Persistence.MongoDb.Attributes;
global using SparkPlug.Persistence.MongoDb.Model;
global using SparkPlug.Persistence.MongoDb.Context;
global using SparkPlug.Persistence.MongoDb.Repository;
global using SparkPlug.Persistence.MongoDb.HealthCheck;
global using SparkPlug.Persistence.MongoDb.Configuration;
