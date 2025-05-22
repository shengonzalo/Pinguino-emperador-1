# OGA.AAS.Backend

# Docker

docker build --rm -t oga.aas.backend --build-arg FEED_ACCESSTOKEN="" .
docker run --rm -d -it -p 5000:5000 oga.aas.backend