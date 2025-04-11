#!/bin/sh

# Esperar a que MySQL esté listo
host="$1"
shift
cmd="$@"

until nc -z "$host" 3306; do
  echo "Esperando a que MySQL esté disponible en $host:3306..."
  sleep 1
done

exec $cmd