using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ExampleSPA.Shared.Models;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace ExampleSPA.Client.Pages
{
    public class ConsultantDataModel : BlazorComponent
    {
        [Inject]
        protected HttpClient Http { get; set; }
        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Parameter]
        protected string action { get; set; }

        protected List<Consultants> consultantList = new List<Consultants>();
        protected Consultants consultant = new Consultants();
        protected string title { get; set; }

        protected async Task FetchConsultant()
        {
            title = "Consultores";
            consultantList = await Http.GetJsonAsync<List<Consultants>>("api/Consultants");
        }

        protected override async Task OnParametersSetAsync()
        {
            if (action == "fetch")
            {
                await FetchConsultant();
                this.StateHasChanged();
            }
        }
    }
}