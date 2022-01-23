using System.Collections.Generic;

public class GeneratedForm
{
    public List<FormResponse> Responses { get; set; } = new List<FormResponse>();
}

public class FormResponse
{
    public FormQuestion QuestionReference;
    public string Response;
}