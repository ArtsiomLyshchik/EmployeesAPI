namespace EmployeesAPI.RestAPI.Extensions;

public static class GenericTypeExtensions
{
    public static string GetGenericTypeName(this Type type)    
    {
        var typeName = string.Empty;
        if (type.IsGenericType)        {
            var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());            
            typeName = $"{type.Name.Remove(type.Name.IndexOf('`', StringComparison.InvariantCultureIgnoreCase))}<{genericTypes}>";
        }        
        else
        {            
            typeName = type.Name;
        }
        
        return typeName;
    }
    
    public static string GetGenericTypeName(this object obj) => obj.GetType().GetGenericTypeName();
}