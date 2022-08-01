<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <KeepModal />
  </main>
  <footer>

  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
    name: "App",
    setup() {
        return {
            appState: computed(() => AppState)
        };
    },
    // components: { KeepModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

</style>
