const callAuthProvider = (provider) => {
    window.location.href = `External/${provider}${window.location.search}`;
}