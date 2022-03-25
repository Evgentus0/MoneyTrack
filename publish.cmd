docker build -t moneytrack-api .
docker tag moneytrack-api registry.heroku.com/moneytrack-api/web
docker push registry.heroku.com/moneytrack-api/web
heroku container:release web --app moneytrack-api