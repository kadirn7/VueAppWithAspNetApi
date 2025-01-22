<template>
  <div class="home-wrapper">
    <nav class="navbar">
      <div class="nav-brand">
        <h3>VueApp</h3>
      </div>
      <div class="nav-menu">
        <span class="welcome-text">Hoş geldiniz, {{ username }}</span>
        <button @click="handleLogout" class="logout-button">Çıkış Yap</button>
      </div>
    </nav>

    <div class="content">
      <h1>Ana Sayfa</h1>
      <p>Başarıyla giriş yaptınız!</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const username = ref('')

onMounted(() => {
  const currentUser = localStorage.getItem('currentUser')
  if (!currentUser) {
    router.push('/login')
    return
  }
  
  const user = JSON.parse(currentUser)
  username.value = user.username
})

const handleLogout = () => {
  localStorage.removeItem('currentUser')
  router.push('/login')
}
</script>

<style scoped>
.home-wrapper {
  min-height: 100vh;
  background-color: #f8f7fa;
}

.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
  background-color: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.nav-brand h3 {
  color: #696cff;
  font-size: 1.5rem;
  font-weight: 600;
  margin: 0;
}

.nav-menu {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.welcome-text {
  color: #5d596c;
  font-size: 0.875rem;
}

.logout-button {
  padding: 0.5rem 1rem;
  background-color: #ff4d49;
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 0.875rem;
  cursor: pointer;
  transition: background-color 0.15s ease-in-out;
}

.logout-button:hover {
  background-color: #e04440;
}

.content {
  padding: 2rem;
  max-width: 1200px;
  margin: 0 auto;
}

.content h1 {
  color: #32325d;
  margin-bottom: 1rem;
}

.content p {
  color: #6e6b7b;
}
</style> 