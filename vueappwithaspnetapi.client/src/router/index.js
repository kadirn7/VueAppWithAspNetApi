import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Login.vue'
import Register from '../views/Register.vue'
import Home from '../views/Home.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      redirect: '/home'
    },
    {
      path: '/login',
      component: Login,
      meta: { requiresGuest: true }
    },
    {
      path: '/register',
      component: Register,
      meta: { requiresGuest: true }
    },
    {
      path: '/home',
      component: Home,
      meta: { requiresAuth: true }
    }
  ]
})

router.beforeEach((to, from, next) => {
  const isAuthenticated = !!localStorage.getItem('currentUser')

  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login')
  } 
  else if (to.meta.requiresGuest && isAuthenticated) {
    next('/home')
  }
  else {
    next()
  }
})

export default router 