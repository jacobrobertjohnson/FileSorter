# FileSorter
## Background
This summer, I finally went through my box of random PC parts that's been filling up since high school. Among those parts were three or four hard drives containing nearly 20 years' worth of family photos. Each drive's photos duplicated a subset of the others, with most of them poorly organized and scattered across multiple user profiles.

The purpose of this project was to make some sense out of that mess in three ways:

1. Finding all of the photos across the drives
1. Removing duplicates from the photos that are found
1. Organizing the photos by the year and month in which they were taken

## Usage
The directories in which to search for photos will need to reside under a single source directory. Once that is in place, FileSorter can be run as follows:

```
FileSorter.exe [sourceDirectory] [targetDirectory] [searchPattern]
```

|Argument|Description|
|---|---|
|`[sourceDirectory]`|The directory under which to search for photos. Trailing backslashes are optional.|
|`[targetDirectory]`|The directory into which the photos will be sorted. Trailing backslashes are optional.|
|`[searchPattern]`|A comma-delimited list of file extensions which will be found and sorted. Matches are case-insensitive.|

For example, to find all of the `.JPG` and `.PNG` files in `C:\messyPhotoDump\` and sort them into `D:\niceCleanPhotoFolder\`, run:

```
FileSorter.exe C:\messyPhotoDump\ D:\niceCleanPhotoFolder JPG,PNG
```

**Note:** As with all Windows command line apps, paths containing spaces will need to be wrapped in quotes:

```
FileSorter.exe "C:\messy photo dump\" "D:\nice clean photo folder" JPG,PNG
```

## Sorting
EXIF data is used to determine the date & time a photo was taken. If the [`PropertyTagExifDTOrig`](https://docs.microsoft.com/en-us/windows/win32/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagexifdtorig) attribute is available and populated, then it is used. Otherwise, the process falls back to [`PropertyTagDateTime`](https://docs.microsoft.com/en-us/windows/win32/gdiplus/-gdiplus-constant-property-item-descriptions#propertytagdatetime)

## Logging
Every FileSorter run creates a timestamped log file under `{{FileSorterExePath}}\Logs\`:

![image](https://user-images.githubusercontent.com/75222785/141515469-4a80dd45-fdfc-461e-bf03-ccd87377bfde.png)

![image](https://user-images.githubusercontent.com/75222785/141514018-8b5070f5-a54f-4c89-8586-b8d5a059af18.png)

The file contains everything that was output to the screen during its corresponding run. This includes metadata about the ongoing operation, and any Exceptions that occur while searching for or copying files or directories:

```
Started searching for files at 8/18/2021 7:06:40 AM

[FILE SEARCH ERROR]
- File: C:\Sample Path\poorlyFormattedImage.png
Parameter is not valid.

Finished searching for files at 8/18/2021 7:35:57 AM

28077 files will be copied.

Finished copying files at 8/18/2021 8:08:35 AM
```
