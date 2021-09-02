using BottleShop.Services;
using BottleShop.Storage;
using BottleShop.Storage.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BottleShop
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BottleShop", Version = "v1" });
			});

			RegisterBottleShopServices(services);

			ConfigureJson();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BottleShop v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void ConfigureJson()
		{
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};
		}

		private void RegisterBottleShopServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(ServiceMapperProfile), typeof(StorageMapperProfile));

			services.AddSingleton(new StorageConfiguration()
			{
				ConnectionString = Configuration.GetConnectionString("AzureStorage"),
			});

			services.AddScoped<ITableRepository<ProductTableEntity>, TableRepository<ProductTableEntity>>();
			services.AddScoped<ITableRepository<PromotionTableEntity>, TableRepository<PromotionTableEntity>>();
			services.AddScoped<ITableRepository<TrolleyTableEntity>, TableRepository<TrolleyTableEntity>>();

			services.AddScoped<IProductRepository, ProductTableRepository>();
			services.AddScoped<IPromotionRepository, PromotionTableRepository>();
			services.AddScoped<ITrolleyRepository, TrolleyTableRepository>();

			services.AddScoped<ITrolleyPromotionService, TrolleyPromotionService>();
			services.AddScoped<IPromotionService, PromotionService>();
			services.AddScoped<ITrolleyService, TrolleyService>();
		}
	}
}
