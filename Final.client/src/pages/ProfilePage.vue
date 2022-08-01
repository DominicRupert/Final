<template>
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h1>Profile Page</h1>
        <h2>{{profile.name}}</h2>
      </div>
    </div>

    <div class="masonry-frame">
      <h3>Keeps: {{keeps.length}}</h3>
      <div class="masonry-grid">
        <div class="masonry-frame">
          <div v-for="keep in keeps" :key="keep.id">
            <Keep :keep="keep" />
          </div>
        </div>
      </div>
      <div class="col-md-8"></div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState.js'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger.js'
import { profilesService } from '../services/ProfilesService.js'
import { keepsService } from '../services/KeepsService.js'

import Pop from '../utils/Pop.js'
import Keep from '../components/Keep.vue'



export default {
  name: 'Profile',
  setup() {
    const route = useRoute();
    onMounted(async () => {
      try {
        await profilesService.getProfile(route.params.id);

        await keepsService.getKeepsByProfile(route.params.id);
      }
      catch (error) {
        logger.error(error);
        Pop.toast(error.message);
      }
    });
    return {
      route,
      profilekeeps: computed(() => AppState.profileKeeps),
      keeps: computed(() => AppState.keeps),

      profile: computed(() => AppState.activeProfile),
    };
  },
  components: { Keep }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  margin-top: 20px;
  margin-bottom: 20px;
  display: grid;
  padding: 2em;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  grid-auto-rows: minmax(100px, auto);
}
</style>