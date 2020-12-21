using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSM.DAL;
using DSM.DAL.Helpers;
using DSM.DBModels;
using DSM.Interface;
using DSM.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace DSM
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #region local
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
            Path.Combine("C:\\DMS", "")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DSM", Version = "v7" });
            });

            //var connection = @"Server=DESKTOP-77NC6JC;Database=DSM;user id=sa;password=srks4$;";

          //  var connection = @"Server=DESKTOP-N18DDM0\SQLEXPRESS;Database=DSM;user id=sa;password=srks4$;";

           var connection = @"Server=TCP:13.233.129.21,8090;Database=DSM;user id=sa;password=srks4$;";
            services.AddDbContext<DSMContext>(options => options.UseSqlServer(connection));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var commonEmailKey = (appSettings.CommonEmail);
            var documentEmail = (appSettings.DocumentEmail);
            var resetLinkURL = (appSettings.ResetLinkURL);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #region configure DI for application services

            services.AddScoped<IUserService, UserService>();

            services.AddTransient<IAsset, AssetDAL>();
            services.AddTransient<ICheckListActivityMaster, CheckListActivityMasterDAL>();
            services.AddTransient<ICheckListJobActivityMaster, CheckListJobActivityMasterDAL>();
            services.AddTransient<ICheckListJobActivityOperator, CheckListJobActivityOperatorDAL>();
            services.AddTransient<ICheckListAdvanceMaster, CheckListAdvanceMasterDAL>();
            services.AddTransient<ICheckListJobAdvanceMaster, CheckListJobAdvanceMasterDAL>();
            services.AddTransient<ICheckListJobAdvanceOperator, CheckListJobAdvanceOperatorDAL>();
            services.AddTransient<ICheckListCategoryMaster, CheckListCategoryMasterDAL>();
            services.AddTransient<ICheckListGroupMaster, CheckListGroupMasterDAL>();
            services.AddTransient<ICheckListLOTOTOMaster, CheckListLOTOTOMasterDAL>();
            services.AddTransient<ICheckListJobLOTOTOMaster, CheckListJobLOTOTOMasterDAL>();
            services.AddTransient<ICheckListJobLOTOTOOperator, CheckListJobLOTOTOOperatorDAL>();
            services.AddTransient<ICheckListMaster, CheckListMasterDAL>();
            services.AddTransient<ICheckListJobAssignedResourceMaster, CheckListJobAssignedResourceMasterDAL>();
            services.AddTransient<ICheckListJobMaster, CheckListJobMasterDAL>();
            services.AddTransient<ICheckListJobOperator, CheckListJobOperatorDAL>();
            services.AddTransient<ICheckListSubCategoryMaster, CheckListSubCategoryMasterDAL>();
            services.AddTransient<ICheckListSupervisorApproval, CheckListSupervisorApprovalDAL>();
            services.AddTransient<ICheckListTypeMaster, CheckListTypeMasterDAL>();
            services.AddTransient<IColorMaster, ColorMasterDAL>();
            services.AddTransient<ILogin, LoginDAL>();
            services.AddTransient<ILineNumberMaster, LineNumberMasterDAL>();
            services.AddTransient<IDepartment, DepartmentDAL>();
            services.AddTransient<IDesignation, DesignationDAL>();
            services.AddTransient<IDocumentMaster, DocumentMasterDAL>();
            services.AddTransient<IDocumentUploader, DocumentUploaderDAL>();
            services.AddTransient<IGradeMaster, GradeMasterDAL>();
            services.AddTransient<IRole, RoleDAL>();
            services.AddTransient<IReports, ReportsDAL>();
            services.AddTransient<IShiftMaster, ShiftMasterDAL>();
            services.AddTransient<ITargetOverAll, TargetOverAllDAL>();
            services.AddTransient<IUserManagement, UserManagementDAL>();
            #endregion
            #endregion
            #region server
            //services.AddSingleton<IFileProvider>(
            //new PhysicalFileProvider(
            //Path.Combine("C:\\DMS", "")));

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "DSM", Version = "v7" });
            //});

            //var connection = @"Server=TCP:13.233.129.21,8090;Database=DSM;user id=sa;password=srks4$;";
            //services.AddDbContext<DSMContext>(options => options.UseSqlServer(connection));

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllOrigins",
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin();
            //            builder.AllowAnyHeader();
            //            builder.AllowAnyMethod();
            //        });
            //});

            //// configure strongly typed settings objects
            //var appSettingsSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            //// configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //var commonEmailKey = (appSettings.CommonEmail);
            //var documentEmail = (appSettings.DocumentEmail);
            //var resetLinkURL = (appSettings.ResetLinkURL);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            //#region configure DI for application services

            //services.AddScoped<IUserService, UserService>();

            //services.AddTransient<IAsset, AssetDAL>();
            //services.AddTransient<ICheckListActivityMaster, CheckListActivityMasterDAL>();
            //services.AddTransient<ICheckListJobActivityMaster, CheckListJobActivityMasterDAL>();
            //services.AddTransient<ICheckListJobActivityOperator, CheckListJobActivityOperatorDAL>();
            //services.AddTransient<ICheckListAdvanceMaster, CheckListAdvanceMasterDAL>();
            //services.AddTransient<ICheckListJobAdvanceMaster, CheckListJobAdvanceMasterDAL>();
            //services.AddTransient<ICheckListJobAdvanceOperator, CheckListJobAdvanceOperatorDAL>();
            //services.AddTransient<ICheckListCategoryMaster, CheckListCategoryMasterDAL>();
            //services.AddTransient<ICheckListGroupMaster, CheckListGroupMasterDAL>();
            //services.AddTransient<ICheckListLOTOTOMaster, CheckListLOTOTOMasterDAL>();
            //services.AddTransient<ICheckListJobLOTOTOMaster, CheckListJobLOTOTOMasterDAL>();
            //services.AddTransient<ICheckListJobLOTOTOOperator, CheckListJobLOTOTOOperatorDAL>();
            //services.AddTransient<ICheckListMaster, CheckListMasterDAL>();
            //services.AddTransient<ICheckListJobAssignedResourceMaster, CheckListJobAssignedResourceMasterDAL>();
            //services.AddTransient<ICheckListJobMaster, CheckListJobMasterDAL>();
            //services.AddTransient<ICheckListJobOperator, CheckListJobOperatorDAL>();
            //services.AddTransient<ICheckListSubCategoryMaster, CheckListSubCategoryMasterDAL>();
            //services.AddTransient<ICheckListSupervisorApproval, CheckListSupervisorApprovalDAL>();
            //services.AddTransient<ICheckListTypeMaster, CheckListTypeMasterDAL>();
            //services.AddTransient<IColorMaster, ColorMasterDAL>();
            //services.AddTransient<ILogin, LoginDAL>();
            //services.AddTransient<ILineNumberMaster, LineNumberMasterDAL>();
            //services.AddTransient<IDepartment, DepartmentDAL>();
            //services.AddTransient<IDesignation, DesignationDAL>();
            //services.AddTransient<IDocumentMaster, DocumentMasterDAL>();
            //services.AddTransient<IDocumentUploader, DocumentUploaderDAL>();
            //services.AddTransient<IGradeMaster, GradeMasterDAL>();
            //services.AddTransient<IHelpContent, HelpContentDAL>();
            //services.AddTransient<IRole, RoleDAL>();
            //services.AddTransient<IReports, ReportsDAL>();
            //services.AddTransient<IShiftMaster, ShiftMasterDAL>();
            //services.AddTransient<ITargetOverAll, TargetOverAllDAL>();
            //services.AddTransient<IUserManagement, UserManagementDAL>();
            //#endregion
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors("AllowAllOrigins");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DefaultModelsExpandDepth(-1);
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.Use(async (context, next) => {
                context.Request.EnableRewind();
                await next();
            });

            app.UseAuthentication();

            //  loggerFactory.AddLog4Net();

            // loggerFactory.AddLog4Net();
        }
    }
}
