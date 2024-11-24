using Demo.WebApp.Classes;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace Demo.WebApp.Components.Controls
{
    public partial class LabeledTextbox : UnqComponentBase
    {
        #region properties
        #endregion properties

        #region parameters

        [Parameter]
        public string? ID { get; set; } = "txLabeledTextBox";

        [Parameter]
        public bool IsPassword { get; set; } = false;

        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public string? Name { get; set; }

        [Parameter]
        public string Value { get; set; }
        string? _value;

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public Expression<Func<string>> ValueExpression { get; set; }

        #endregion parameters

        #region data
        #endregion data

        #region lifecycle
        #endregion lifecycle

        #region event handlers

        void textbox_OnChange()
        {
            if (this.ValueChanged.HasDelegate)
            {
                this.ValueChanged.InvokeAsync(this.Value);
            }
        }

        #endregion event handlers

        #region private
        #endregion private

        #region public
        #endregion public
    }
}
