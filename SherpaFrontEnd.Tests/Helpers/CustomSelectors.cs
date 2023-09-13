using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Tests.Helpers;

public static class CustomSelectors
{
    public static IElement? FindElementByCssSelectorAndTextContent<T>(this IRenderedComponent<T> renderedComponent,
        string cssSelector, string textContent) where T : IComponent
    {
        return renderedComponent.FindAll(cssSelector)
            .FirstOrDefault(element => element.TextContent.Contains(textContent));
    }
}