const callAuthProvider = (provider) => {
    const urlParams = new URLSearchParams(window.location.search);
    const redirect_uri = urlParams.get('redirect_uri');
    console.log(redirect_uri)
    window.location.href = `External/${provider}?returnUrl=${redirect_uri}`;
}