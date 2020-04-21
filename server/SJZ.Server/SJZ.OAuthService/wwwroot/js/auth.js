const callAuthProvider = (provider) => {
    const urlParams = new URLSearchParams(window.location.search);
    const returnUrl = urlParams.get('ReturnUrl');
    console.log(redirect_uri)
    window.location.href = `External/${provider}?returnUrl=${returnUrl}`;
}