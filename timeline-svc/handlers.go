package main

import (
	// "encoding/json"
	// "fmt"
	"net/http"
	// "github.com/gorilla/mux"
	// "github.com/gorilla/schema"
)

func health(w http.ResponseWriter, r *http.Request) {
	w.Write([]byte("I am fine."))
}
