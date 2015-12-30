#!/bin/sh
echo "" > .env
for x in `heroku config -s`; do
  if [[ $x == DATABASE_URL* ]]; then
	  echo $x?ssl=true >> .env
  elif [ $x != '=>' ]; then
	  echo $x >> .env
  fi
done
