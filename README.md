aaTemplateExtract
=================

A tool to extract scripts and attributes from ArchestrA Galaxy templates and output them to human (and version control) readable XML.

## Motivation

Wonderware ArchestrA System Platform templates can only be natively exported to a binary package. To keep track of changes in scripts that I've written inside of templates, I typically have copy/pasted the contents to plain text files and then checked those into my version control system. This is a tedious process that is prone to error and forgetfulness. In a few seconds, this tool will extract this information and format into version control friendly XML files.

## Installation

Because of GR Access Toolkit licensing, I cannot distribute a binary executable format of this program. You'll need to download and install the GR Access and then build the program yourself. GR Access is available from the [Wonderware Developer Network Downloads](https://wdn.wonderware.com/sites/WDN/Pages/Downloads/software.aspx). Look for **Wonderware GR Access Toolkit 2014** under **Wonderware System Platform**.

I built this using Visual Studio 2013, using .NET 4.5.

## Platforms

I have only currently tested this on System Platform 2014, running on Windows Server 2012, 64-bit. However, I am building the package as an x86 target because of GR Access's build.

## TODO List

See my current roadmap and overall [TODO list](/TODO.md)

## Contributors

* [Eliot Landrum](mailto:elandrum@stonetek.com), MES Analyst with [Stone Technologies](http://stonetek.com).

## License

MIT License. See the [LICENSE file](/LICENSE) for details.
