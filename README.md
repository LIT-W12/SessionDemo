# Session Demo

You need to add the following lines of code to enable Session:

https://github.com/LIT-W12/SessionDemo/blob/master/SessionDemo.Web/Program.cs#L10

https://github.com/LIT-W12/SessionDemo/blob/master/SessionDemo.Web/Program.cs#L21

Also, to add any arbitrary C# object to session, use the following extension method (you'll need to add a using statement on top `using System.Text.Json;`):

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
