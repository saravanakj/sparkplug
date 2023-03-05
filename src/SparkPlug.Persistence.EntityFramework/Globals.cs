global using System;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Data.Common;
global using System.Threading;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.JsonPatch;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Newtonsoft.Json;

global using SparkPlug.Contracts;
global using SparkPlug.Persistence.Abstractions;

global using SparkPlug.Persistence.EntityFramework;
global using SparkPlug.Persistence.EntityFramework.Context;
global using SparkPlug.Persistence.EntityFramework.HealthCheck;
global using SparkPlug.Persistence.EntityFramework.Configuration;
global using SparkPlug.Persistence.EntityFramework.Models;