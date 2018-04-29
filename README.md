# Link Downloader
This is a C# Console application that downloads files from the web using links in an XML file or direct input, respecting the concurrency limit provided.

## Motivation
Sometimes you need files, photos or clips from the web for personal use, and download links to these files sometimes happen to follow a numeric sequence, such as

* [..Pictures/Moon1.jpg](https://raw.githubusercontent.com/hakanyildizhan/LinkDownloader/master/SamplePictures/moon1.jpg)
* [..Pictures/Moon2.jpg](https://raw.githubusercontent.com/hakanyildizhan/LinkDownloader/master/SamplePictures/moon2.jpg)
* [..Pictures/Moon3.jpg](https://raw.githubusercontent.com/hakanyildizhan/LinkDownloader/master/SamplePictures/moon3.jpg)
* ... and so on

It is tiresome to input each link in the address bar and initiate downloads manually, so you could simply do this:

**..Pictures/Moon&ast;.jpg [1-3]**

The pattern is applied on the asterisked part of the link.

## How To Use
Provide links at the console separated by semicolon (;) or place the links in the Links.xml in the provided structure.

For pattern matching, simply put an asterisk (&ast;) on the numeric part that changes, and then the numeric range at the end in brackets.

Works with numerics with leading zeroes as well. (such as 01, 02.. etc)

Just make sure the xml file is in the same folder as the executable.

Refer to app.config in order to change additional settings, including number of simultaneous downloads.

## Features

* Get links from either direct input on console or xml file
* Download direct links as well as generating incremental links from patterns such as [1-10], [02-25] etc.
* Check and report broken links, before initiating jobs
* Respect maximum simultaneous download limit

## Development and contributing

Feel free to send pull requests and raise issues.