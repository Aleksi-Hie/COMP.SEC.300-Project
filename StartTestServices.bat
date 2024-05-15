cd ".\Test services"\webdev2\"
docker compose up -d
cd ..\..\

cd ".\Test services"\gruyere-code\"
docker build . -t grueye
docker run -d -p 8008:8008 grueye