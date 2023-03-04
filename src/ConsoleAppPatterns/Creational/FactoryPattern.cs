using System;
using System.Collections.Generic;

namespace ConsoleAppPatterns.Creational;

public class FactoryPattern
{
	public void Main()
	{
		var documents = new List<Document>
		{
			new Book(),
			new Resume()
		};

		foreach (var document in documents)
		{
			document.CreatePages();

			Console.WriteLine(document.GetType().Name);

			foreach (var page in document.Pages)
			{
				Console.WriteLine(page.GetType().Name);
			}
		}
	}
}

public abstract class Page
{
}

public class Skills : Page
{
}

public class Education : Page
{
}

public class Content : Page
{
}

public class Introduction : Page
{
}

public class ParagraphOne : Page
{
}

public class Ending : Page
{
}

public class Summary : Page
{
}

public abstract class Document
{
	public List<Page> Pages = new List<Page>();

	public abstract void CreatePages();
}

public class Book : Document
{
	public override void CreatePages()
	{
		Pages.Add(new Content());
		Pages.Add(new Introduction());
		Pages.Add(new ParagraphOne());
		Pages.Add(new Ending());
		Pages.Add(new Summary());
	}
}

public class Resume : Document
{
	public override void CreatePages()
	{
		Pages.Add(new Skills());
		Pages.Add(new Education());
		Pages.Add(new Summary());
	}
}