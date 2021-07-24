using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace Homuai.Api.Configuration.Swagger
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();

                foreach (var item in Enum.GetValues(context.Type))
                {
                    var value = (int)item;
                    var name = Enum.GetName(context.Type, item);

                    schema.Enum.Add(new OpenApiString($"{value} - {name}"));
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SwaggerSubtypeOfAttributeFilter : ISchemaFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var subTypes = context
                .Type
                .GetTypeInfo()
                .GetCustomAttributes<SwaggerSubtypeOfAttribute>();

            if (subTypes == null || !subTypes.Any()) return;

            foreach (var subType in subTypes)
            {
                if (!context.SchemaRepository.Schemas.ContainsKey(subType.Parent.Name))
                    context.SchemaGenerator.GenerateSchema(subType.Parent, context.SchemaRepository);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SwaggerSubtypeOfAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type Parent { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parent"></param>
        public SwaggerSubtypeOfAttribute(string name, Type parent)
        {
            this.Name = name;
            this.Parent = parent;
        }
    }
}
