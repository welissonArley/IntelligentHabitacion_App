![Maintained](https://img.shields.io/badge/Maintained%3F-yes-green.svg?style=for-the-badge)
[![Contributors][contributors-shield]][contributors-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
    <li><a href="#build-status">Build Status</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

[![Homuai Screen Shot][product-screenshot]](https://example.com)

When I was living with some friends (three to be more specific) I used to have some problems with the organization and the houseworks. Unfortunately, student life is so busy and it’s normal to forget things like: paying rent; and letting products expire in the refrigerator.

I didn’t find one good App that really suited my needs, so I thought: why can’t I make a project to solve this myself? Homuai is an app free of charge whose main goal is to help people who live with friends. It was a way that I found as a developer to create an opportunity to practice and improve my skills and learn new ones.

Of course, your needs may be different and I can’t promise you that this project is the best in the entire world. So I'll be adding more features in the near future. You may also suggest changes by forking this repo and creating a pull request or opening an issue.

### Built With

![windows-shield] ![ubuntu-shield] ![figma-shield] ![visualstudio-shield] ![netcore-shield] ![xamarin-shield] ![android-shield] ![ios-shield] ![mysql-shield] ![aws-shield]

<!-- GETTING STARTED -->
## Getting Started

You can download the App for free on:

[![google-play-shield]](https://play.google.com/store/apps/details?id=com.id1tech.homuai.app)
[![app-store-shield]](https://example.com)

To get a local copy up and running follow these simple example steps.

### Prerequisites

* SDK Android API 23 or higher to run the Android App version

* MAC with the iOS SDK 10 (just in case you want to run the iOS App version)

* Visual Studio 2019+

* MySQL Server

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/welissonArley/Homuai.git
   ```
2. Fill all information in `appsettings.Development.json`. NOTE: In the section ConnectionStrings:
   ```sh
    {
        "ConnectionStrings":
        {
            "Connection": "Server=localhost;Uid=root;Pwd=@password;",
            "DatabaseName": "homuai-database-name"
        },
        ...
    }
    ```
3. Run the Web API

To run the app using the API locally do the follow:

1. Download and configure ngrok
    ```sh
       https://ngrok.com
   ```
2. Run ngrok;

3. Change the api link in `RestEndPoints.cs` on the app project to the link shown in the console (the console that you are running ngrok)

4. Have a good time testing.

<!-- ROADMAP -->
## Roadmap

You can see the [open issues](https://github.com/welissonArley/Homuai/issues) (and known issues) and the [board project](https://github.com/welissonArley/Homuai/projects/1) to see the future features available on the project.

<!-- LICENSE -->
## License

The Homuai Project can not be copied and/or distributed without the express permission of Welisson Arley <welissonarleyvs@gmail.com>.

Feel free to use this project to study and help me improve them by becoming a contributor :smile:

<!-- CONTACT -->
## Contact

Support: homuai@homuai.com

Website: comming soon

<!-- ACKNOWLEDGEMENTS -->
## Acknowledgements
* [Othneil Drew for this incredible Read-me template](https://github.com/othneildrew/Best-README-Template)
* [Willian Rodrigues with the tests](https://www.linkedin.com/in/willian-rodrigues-b99b76b7/)
* [Henrique Couto with the tests](https://www.linkedin.com/in/henrique-couto-3287b1133/)
* [Marina Moreira with the translations](https://www.linkedin.com/in/marina-moreira-54b4b116a/)

<!-- Build Status (Badges) -->
## Build Status
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=welissonArley_Homuai&metric=alert_status)](https://sonarcloud.io/dashboard?id=welissonArley_Homuai)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=welissonArley_Homuai&metric=bugs)](https://sonarcloud.io/dashboard?id=welissonArley_Homuai)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=welissonArley_Homuai&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=welissonArley_Homuai)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=welissonArley_Homuai&metric=code_smells)](https://sonarcloud.io/dashboard?id=welissonArley_Homuai)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=welissonArley_Homuai&metric=coverage)](https://sonarcloud.io/dashboard?id=welissonArley_Homuai)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=welissonArley_Homuai&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=welissonArley_Homuai)

<!-- MARKDOWN LINKS & IMAGES -->
[product-screenshot]: readme-images/screenshot.png
[contributors-shield]: https://img.shields.io/github/contributors/welissonArley/Homuai.svg?style=for-the-badge
[contributors-url]: https://github.com/welissonArley/Homuai/graphs/contributors
[stars-shield]: https://img.shields.io/github/stars/welissonArley/Homuai.svg?style=for-the-badge
[stars-url]: https://github.com/welissonArley/Homuai/stargazers
[issues-shield]: https://img.shields.io/github/issues/welissonArley/Homuai.svg?style=for-the-badge
[issues-url]: https://github.com/welissonArley/Homuai/issues
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/welissonarley/
[xamarin-shield]: https://img.shields.io/badge/Xamarin-3498DB?style=for-the-badge&logo=xamarin&logoColor=white
[netcore-shield]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[mysql-shield]: https://img.shields.io/badge/MySQL-00000F?style=for-the-badge&logo=mysql&logoColor=white
[aws-shield]: https://img.shields.io/badge/Amazon_AWS-232F3E?style=for-the-badge&logo=amazon-aws&logoColor=white
[android-shield]: https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white
[ios-shield]: https://img.shields.io/badge/iOS-000000?style=for-the-badge&logo=ios&logoColor=white
[windows-shield]: https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white
[ubuntu-shield]: https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white
[visualstudio-shield]: https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white
[figma-shield]: https://img.shields.io/badge/Figma-F24E1E?style=for-the-badge&logo=figma&logoColor=white
[buymecoffe-shield]: https://img.shields.io/badge/Buy_Me_A_Coffee-FFDD00?style=for-the-badge&logo=buy-me-a-coffee&logoColor=black
[google-play-shield]: https://img.shields.io/badge/Google_Play-414141?style=for-the-badge&logo=google-play&logoColor=white
[app-store-shield]: https://img.shields.io/badge/App_Store-0D96F6?style=for-the-badge&logo=app-store&logoColor=white
