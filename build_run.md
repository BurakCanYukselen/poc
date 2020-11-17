**docker build --rm --pull -f "" -t "docker-web-api" ""**
> docker build 
> --rm (Remove intermediate containers after a successful build)
> --pull (Always attempt to pull a newer version of the image)
> -f (Path to a Dockerfile)
> -t (Name and optionally a tag in the ‘name:tag’ format)
> [Path to solution]

**docker run -d --rm -p 5001:443 -p 5000:80 --name docker-web-api docker-web-api**
> -d (Run container in background and print container ID)
> --rm (Automatically remove the container when it exits)
> -p (Publish a container’s port(s) to the host)
>    (Publish all exposed ports to random ports)
> --name (Assign a name to the container)
> [image name]