using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace DotParser.SyntaxAnalysis;

public abstract class BlockBuilder
{
    protected List<Element> elements { get; private set; }

    public bool IsComplited { get; protected set; }

    protected BlockBuilder()
    {
        elements = new List<Element>();
    }

    protected abstract bool IsRelevant(Element element);

    protected abstract bool IsFinishElement(Element element);

    protected virtual void OnElementAdded(Element element) { }

    protected virtual void OnFirstElementAdded(Element element) { }

    public void AddElement(Element element)
    {
        if (!IsRelevant(element))
            throw new DotSyntaxException();
        if (elements.Count == 0)
            OnFirstElementAdded(element);
        elements.Add(element);
        OnElementAdded(element);
        if (IsFinishElement(element))
            IsComplited = true;
    }

    public abstract Block GetResult();

    public abstract class Factory
    {
        public abstract bool IsBeginElement(Element element);

        public abstract BlockBuilder GetBuilder();
    }
}
