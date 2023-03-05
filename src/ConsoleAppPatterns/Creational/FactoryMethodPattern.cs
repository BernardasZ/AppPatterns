using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppPatterns.Creational;

/// <summary>
/// Client.
/// </summary>
public class FactoryMethodPattern
{
	public void Main()
	{
		PrintDocument(new Resume());
		PrintDocument(new Book());
	}

	public void PrintDocument(Document document)
	{
		Console.WriteLine(document.GetDocumentContent());
	}
}

/// <summary>
/// Product.
/// </summary>
public interface IPage
{
	string GetPageContent();
}

public class Skills : IPage
{
	public string GetPageContent() => nameof(Skills);
}

public class Education : IPage
{
	public string GetPageContent() => nameof(Education);
}

public class Content : IPage
{
	public string GetPageContent() => nameof(Content);
}

public class Introduction : IPage
{
	public string GetPageContent() => nameof(Introduction);
}

public class ParagraphOne : IPage
{
	public string GetPageContent() => nameof(ParagraphOne);
}

public class Ending : IPage
{
	public string GetPageContent() => nameof(Ending);
}

public class Summary : IPage
{
	public string GetPageContent() => nameof(Summary);
}

/// <summary>
/// Creator.
/// </summary>
public abstract class Document
{
	public abstract List<IPage> CreateDocumentPages();

	public abstract string GetName();

	public string GetDocumentContent()
	{
		var pages = CreateDocumentPages();
		var builder = new StringBuilder();

		builder
			.Append(GetName())
			.AppendLine()
			.AppendLine()
			.Append(AppendDocumentContent(pages));

		return builder.ToString();
	}

	private string AppendDocumentContent(List<IPage> pages)
	{
		var builder = new StringBuilder();

		foreach (var page in pages)
		{
			builder
				.Append(" * ")
				.Append(page.GetPageContent())
				.AppendLine();
		}

		return builder.ToString();
	}
}


public class Book : Document
{
	public override List<IPage> CreateDocumentPages() => new List<IPage>
		{
			new Content(),
			new Introduction(),
			new ParagraphOne(),
			new Ending(),
			new Summary()
		};

	public override string GetName() => nameof(Book);
}

public class Resume : Document
{
	public override List<IPage> CreateDocumentPages() => new List<IPage>
		{
			new Skills(),
			new Education(),
			new Summary()
		};

	public override string GetName() => nameof(Resume);
}