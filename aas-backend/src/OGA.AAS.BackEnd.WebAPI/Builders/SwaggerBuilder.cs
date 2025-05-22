namespace OGA.AAS.BackEnd.WebAPI.Builders
{
    public static class SwaggerBuilder
    {
        public static IApplicationBuilder AddSwaggerApp(this IApplicationBuilder app)
        {
            var enabled = Convert.ToBoolean(Application.Registration.ConfigurationManager.SwaggerEnabled);

            if (((WebApplication)app).Environment.IsDevelopment() && enabled)
            {   
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}
