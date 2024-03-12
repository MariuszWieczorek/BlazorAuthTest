using MwTech.Blazor.Client.Sandbox.Models;
using Microsoft.AspNetCore.Components;

namespace MwTech.Blazor.Client.Sandbox.Pages
{
    public partial class KanbanItem
    {
        private string _classes = "";

        [Parameter]
        public State State { get; set; }

        [Parameter]
        public List<Item> Items { get; set; } = new();

        [Parameter]
        public EventCallback<State> OnDrop { get; set; }

        [Parameter]
        public EventCallback<Item> OnStartDrag { get; set; }

        protected override void OnInitialized()
        {
            switch (State)
            {
                case State.New:
                    _classes = "alert-warning";
                    break;
                case State.Active:
                    _classes = "alert-primary";
                    break;
                case State.Closed:
                    _classes = "alert-success";
                    break;
                default:
                    break;
            }
        }

        private void OnDropHandler()
        {
            OnDrop.InvokeAsync(State);
        }

        private void OnStartDragHandler(Item item)
        {
            OnStartDrag.InvokeAsync(item);
        }
    }
}
