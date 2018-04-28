# Link Downloader
This is a C# Console application that downloads files from the web using links in an XML file or direct input, respecting the concurrency limit provided.

## Motivation
Sometimes you need files, photos or clips from the web for personal use, and download links to these files sometimes happen to follow a numeric sequence, such as

* [Moon1.jpg](https://raw.githubusercontent.com/hakanyildizhan/LinkDownloader/master/SamplePictures/moon1.jpg)
* [Moon2.jpg](https://raw.githubusercontent.com/hakanyildizhan/LinkDownloader/master/SamplePictures/moon2.jpg)
* [Moon3.jpg](https://raw.githubusercontent.com/hakanyildizhan/LinkDownloader/master/SamplePictures/moon3.jpg)
* ... and so on

It is tiresome to input each link in the address bar and initiate downloads manually, so you could simply do this:
**.../moon*.jpg [1-3]**

The pattern is applied on the asterisked part of the link.

## How To Use
Simply provide links at the console separated by semicolon (;) or place the links in the Links.xml in the provided structure.
Just make sure the xml file is in the same folder as the executable.
Refer to app.config in order to change additional settings.

## Features

* Getting links from either direct input on console or xml file
* Downloading direct links as well as generating incremental links from patterns such as [1-10], [02-25] etc.
* Setting path to save files, number of simultaneous downloads and progress report interval for jobs

## Development and contributing

Feel free to send pull requests and raise issues.