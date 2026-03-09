namespace Talabat_Project.Extenssions
{
    public static class SwaggerExtenssion
    {
        public static WebApplication UseSwaggerMiddleWare(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
