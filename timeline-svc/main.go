package main

import (
	"flag"
	"fmt"
	"log"
	"net/http"
	"strings"

	"github.com/rs/cors"
)

func main() {
	port := flag.String("port", ":1000", "http listen port")
	flag.Parse()

	if !strings.HasPrefix(*port, ":") {
		*port = ":" + *port
	}

	log.Println("Starting timeline service...")

	router := NewRouter()
	c := cors.Default().Handler(router)

	log.Println(fmt.Sprintf("Service started at %s", *port))
	log.Fatal(http.ListenAndServe(*port, c))
}
