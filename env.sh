# #!/bin/sh
# for x in `heroku config -s`; do
#   if [[ $x == DATABASE_URL* ]]; then
#       echo $x?ssl=true
# 	#   export $x?ssl=true
# #   elif [[ $x == ASPNET_ENV* ]]; then
# 	  #ignore
#   elif [[ $x != '=>' && $x != ASPNET_ENV* ]]; then
#       echo $x
# 	  export $x
#   fi
# done


export DATABASE_URL=postgres://michaellawrence@localhost:5432/mvc_demo
export ASPNET_ENV=Development
export MGFrom=postmaster@mg.aurlaw.com
export MGFromName=Demo1.MVC Admin
export MGKey=key-39bad8295f1ae64281d0332600e0263c
export MGDomain=mg.aurlaw.com


export MGSMTPHostname=smtp.mailgun.org
export MGSMTPPort=587
export MGSMTPLogin=postmaster@mg.aurlaw.com
export MGSMTPPassword=753b224fb9843350b77c261e9463ea92
