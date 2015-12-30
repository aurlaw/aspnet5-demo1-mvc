#!/bin/sh
for x in `heroku config -s`; do
  if [[ $x == DATABASE_URL* ]]; then
	  export $x?ssl=true
#   elif [[ $x == ASPNET_ENV* ]]; then
	  #ignore
  elif [[ $x != '=>' && $x != ASPNET_ENV* ]]; then
	  export $x
  fi
done
