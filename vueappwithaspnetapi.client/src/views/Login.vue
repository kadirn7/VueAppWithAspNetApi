<template>
  <div class="auth-wrapper">
    <div class="auth-content">
      <div class="auth-form-wrapper">
        <div class="brand-logo">
          <h3>VueApp</h3>
        </div>

        <div class="welcome-text">
          <h4>Hoş Geldiniz! 👋</h4>
          <p>Lütfen hesabınıza giriş yapın</p>
        </div>

        <form @submit.prevent="handleLogin" class="auth-form">
          <div class="form-group">
            <label>Email</label>
            <input 
              type="email" 
              v-model="email"
              required
              placeholder="ornek@email.com"
              class="form-control"
            />
          </div>

          <div class="form-group">
            <div class="password-label">
              <label>Şifre</label>
              <a href="#" class="forgot-link">Şifremi Unuttum?</a>
            </div>
            <div class="password-input">
              <input 
                :type="showPassword ? 'text' : 'password'"
                v-model="password"
                required
                placeholder="············"
                class="form-control"
              />
              <button 
                type="button" 
                class="toggle-password"
                @click="showPassword = !showPassword"
              >
                👁️
              </button>
            </div>
          </div>

          <div class="form-group remember-me">
            <label class="checkbox-container">
              <input type="checkbox" v-model="rememberMe">
              <span class="checkmark"></span>
              Beni Hatırla
            </label>
          </div>

          <div v-if="errorMessage" class="error-message">
            {{ errorMessage }}
          </div>

          <button 
            type="submit"
            :disabled="isLoading"
            class="submit-button"
          >
            <span v-if="isLoading">Yükleniyor...</span>
            <span v-else>Giriş Yap</span>
          </button>

          <div class="register-text">
            <span>Hesabınız yok mu?</span>
            <router-link to="/register" class="register-link">
              Hesap Oluştur
            </router-link>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const email = ref('')
const password = ref('')
const errorMessage = ref('')
const isLoading = ref(false)
const rememberMe = ref(false)
const showPassword = ref(false)

const handleLogin = async () => {
  try {
    isLoading.value = true
    errorMessage.value = ''
    
    const loginData = {
      email: email.value,
      password: password.value
    }

    console.log('İstek URL:', '/api/auth/login')
    console.log('İstek verisi:', loginData)

    const response = await fetch('/api/auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json'
      },
      body: JSON.stringify(loginData)
    })

    console.log('Response status:', response.status)
    console.log('Response headers:', Object.fromEntries(response.headers))

    const responseText = await response.text()
    console.log('Response body:', responseText)

    if (!response.ok) {
      throw new Error(responseText || 'Giriş işlemi başarısız')
    }

    const user = JSON.parse(responseText)
    console.log('Parsed user:', user)

    localStorage.setItem('currentUser', JSON.stringify(user))
    router.push('/home')
  } catch (error) {
    console.error('Hata detayı:', {
      message: error.message,
      stack: error.stack
    })
    errorMessage.value = error.message
  } finally {
    isLoading.value = false
  }
}
</script>

<style scoped>
.auth-wrapper {
  min-height: 100vh;
  display: flex;
  background-color: #f8f7fa;
  position: relative;
}

.auth-content {
  position: absolute;
  right: 0;
  top: 50%;
  transform: translateY(-50%);
  width: 450px;
  padding: 2rem;
}

.auth-form-wrapper {
  background: white;
  border-radius: 8px;
  padding: 2rem;
  width: 100%;
  box-shadow: 0 2px 8px rgba(47, 43, 61, 0.1);
  margin-right: 3rem;
}

.brand-logo {
  text-align: center;
  margin-bottom: 1.5rem;
}

.brand-logo h3 {
  color: #696cff;
  font-size: 1.75rem;
  font-weight: 600;
}

.welcome-text {
  text-align: center;
  margin-bottom: 1.5rem;
}

.welcome-text h4 {
  color: #32325d;
  font-size: 1.5rem;
  font-weight: 600;
  margin-bottom: 0.5rem;
}

.welcome-text p {
  color: #6e6b7b;
  font-size: 0.875rem;
}

.form-group {
  margin-bottom: 1.25rem;
}

.form-group label {
  display: block;
  color: #5d596c;
  font-size: 0.875rem;
  font-weight: 500;
  margin-bottom: 0.5rem;
}

.password-label {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.forgot-link {
  color: #696cff;
  font-size: 0.875rem;
  text-decoration: none;
}

.forgot-link:hover {
  text-decoration: underline;
}

.form-control {
  width: 100%;
  padding: 0.75rem 1rem;
  font-size: 1rem;
  line-height: 1.25;
  color: #32325d;
  background-color: #fff;
  border: 1px solid #dbdade;
  border-radius: 6px;
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
}

.form-control:focus {
  border-color: #696cff;
  outline: 0;
  box-shadow: 0 0 0 0.2rem rgba(105, 108, 255, 0.1);
}

.password-input {
  position: relative;
}

.toggle-password {
  position: absolute;
  right: 1rem;
  top: 50%;
  transform: translateY(-50%);
  background: none;
  border: none;
  color: #6e6b7b;
  cursor: pointer;
  padding: 0;
}

.remember-me {
  display: flex;
  align-items: center;
}

.checkbox-container {
  display: flex;
  align-items: center;
  cursor: pointer;
  font-size: 0.875rem;
  color: #5d596c;
}

.checkbox-container input {
  margin-right: 0.5rem;
}

.submit-button {
  width: 100%;
  padding: 0.75rem;
  background-color: #696cff;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 1rem;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.15s ease-in-out;
}

.submit-button:hover {
  background-color: #5f62ff;
}

.submit-button:disabled {
  background-color: #b3b4ff;
  cursor: not-allowed;
}

.register-text {
  text-align: center;
  margin-top: 1.5rem;
  color: #6e6b7b;
  font-size: 0.875rem;
}

.register-link {
  color: #696cff;
  text-decoration: none;
  margin-left: 0.25rem;
}

.register-link:hover {
  text-decoration: underline;
}

.error-message {
  color: #ff4d49;
  font-size: 0.875rem;
  margin-bottom: 1rem;
  text-align: center;
}
</style> 