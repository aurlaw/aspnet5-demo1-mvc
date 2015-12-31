#!/bin/sh
for x in `heroku config -s`; do
  if [[ $x == DATABASE_URL* ]]; then
      echo $x?ssl=true
	  export $x?ssl=true
#   elif [[ $x == ASPNET_ENV* ]]; then
	  #ignore
  elif [[ $x != '=>' && $x != ASPNET_ENV* ]]; then
      echo $x
	  export $x
  fi
done
