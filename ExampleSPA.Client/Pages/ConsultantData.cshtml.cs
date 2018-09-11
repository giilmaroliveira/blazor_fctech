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
        protected string paramConsultantID { get; set; } = "0";
        [Parameter]
        protected string action { get; set; }

        protected List<Consultant> consultantList = new List<Consultant>();
        protected Consultant consultant = new Consultant();
        protected string title { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (action == "fetch")
            {
                await FetchConsultant();
                this.StateHasChanged();
            }
            else if (action == "create")
            {
                title = "Adicionar Consultor";
                consultant = new Consultant();
            }
            else if (paramConsultantID != "0")
            {
                if (action == "edit")
                {
                    title = "Editar Consultor";
                }
                else if (action == "delete")
                {
                    title = "Remover Consultor";
                }

                consultant = await Http.GetJsonAsync<Consultant>("/api/Consultant/Details/" + Convert.ToInt32(paramConsultantID));
            }
        }

        protected async Task FetchConsultant()
        {
            title = "Consultores";
            consultantList = await Http.GetJsonAsync<List<Consultant>>("/api/Consultant/Index");
        }

        protected async Task CreateConsultant()
        {
            if (consultant.Id != 0)
            {
                await Http.SendJsonAsync(HttpMethod.Put, "/api/Consultant/Edit", consultant);
            }
            else
            {
                await Http.SendJsonAsync(HttpMethod.Post, "/api/Consultant/Create", consultant);
            }
            UriHelper.NavigateTo("/consultant/fetch");
        }

        protected async Task DeleteConsultant()
        {
            await Http.DeleteAsync("/api/Consultant/Delete/" + Convert.ToInt32(paramConsultantID));
            UriHelper.NavigateTo("/consultant/fetch");
        }

        protected void Cancel()
        {
            title = "Consultores";
            UriHelper.NavigateTo("/consultant/fetch");
        }
    }
}