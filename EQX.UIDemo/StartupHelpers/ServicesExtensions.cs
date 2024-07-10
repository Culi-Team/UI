using EQX.UI.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UIDemo.StartupHelpers
{
    public static class ServicesExtensions
    {
        public static void AddFormFactory<TForm>(this IServiceCollection services) where TForm : class
        {
            services.AddTransient<TForm>();
            services.AddSingleton<Func<TForm>>(x => () => x.GetService<TForm>()!);
        } 
    }
}
