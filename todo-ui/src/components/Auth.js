import { useEffect } from "react";

export default function Auth({ isLoading, isRegistering, setIsRegistering, authData, setAuthData, handleLogin, handleRegister }) {
  useEffect(() => {
    setAuthData({ username: "", password: "" })
  }, [])

  return (
    <div className="container">
      <div className="form-card" style={{ maxWidth: '400px', margin: '100px auto' }}>
        <h2>{isRegistering ? "Hesap Oluştur" : "Giriş Yap"}</h2>
        <input value={authData.username} type="text" placeholder="Kullanıcı Adı" onChange={(e) => setAuthData({ ...authData, username: e.target.value })} />
        <input value={authData.password} type="password" placeholder="Şifre" onChange={(e) => setAuthData({ ...authData, password: e.target.value })} />
        <button
          className="add-btn"
          style={{ width: '100%', marginTop: '10px', display: 'flex', justifyContent: 'center', alignItems: 'center', gap: '8px' }}
          onClick={isRegistering ? handleRegister : handleLogin}
          disabled={isLoading} // Yüklenirken butonu tıklanamaz yapar
        >
          {isLoading ? (
            <>
              <div className="spinner"></div> {/* Bu div'e CSS'de şekil vereceğiz */}
              İşlem yapılıyor...
            </>
          ) : (
            isRegistering ? "Kayıt Ol" : "Giriş Yap"
          )}
        </button>
        <p onClick={() => setIsRegistering(!isRegistering)} style={{ cursor: 'pointer', textAlign: 'center', marginTop: '10px' }}>
          {isRegistering ? "Giriş yapın" : "Hesap açın"}
        </p>
      </div>
    </div>
  );
}