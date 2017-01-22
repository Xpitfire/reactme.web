# dork.web
Backend for [dork.app](https://github.com/Xpitfire/dork.app/wiki)

[![Build Status](https://travis-ci.com/Xpitfire/dork.web.svg?token=sHWssDoyyNFRZYWr8UQ5&branch=master)](https://travis-ci.com/Xpitfire/dork.web)

## Getting Started
* Download docker for [Windows](https://docs.docker.com/docker-for-windows/) or [Linux](https://docs.docker.com/engine/installation/linux/ubuntu/) on the official docker site and follow the installation instructions
* Request collaborator access for the official Docker Hub repository [xpitfire/dork.web](https://hub.docker.com/r/xpitfire/dork.web) via slack if not already available
* Run a terminal or PowerShell instance and execute the following command: 
```
docker run -d -p 8181:5000 --name dorkweb xpitfire/dork.web
```

* To start the docker container execute:
```
docker start dorkweb
```

* You can now use the following connection string for local testing: 
```
http://localhost:8181/swagger/ui
```

## Important Links
* [Read Documentation](https://github.com/Xpitfire/dork.web/wiki)
* [Manage Project](https://github.com/Xpitfire/dork.web/projects)
* [Chat on Slack](https://dorkedu.slack.com/)
