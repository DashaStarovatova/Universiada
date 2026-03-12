using System;
using System.Text.Json;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Domain;
using Infrastructure.Services;
using Application.Interfaces;
using System.Globalization;


// cd C:\Users\Darya\Desktop\Работа\Универсиада\Универсиада\Console\
// dotnet add package MediatR
// dotnet add package Microsoft.Extensions.DependencyInjection

class Program
{
    public async static Task Main()
    {        
        // Параметры теста
        double keyRate = 15.5;
        int isRefresh = 0;
        Guid teamID = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var runner = new MatlabRunner();
        var results = await runner.RunMatlabScript(keyRate, isRefresh, teamID);

        Console.WriteLine(results.Length);

    }
};