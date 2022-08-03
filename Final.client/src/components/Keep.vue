<template>

  <div class="selectable card" @click="setActive" data-bs-target="#keep-modal" data-bs-toggle="modal">


    <img :src="keep.img" class="img-fluid card-img" alt="">
    <div class="card-img-overlay">

      <h3 class="card-title txt text-white">{{ keep.name }}
      </h3>
    </div>

  </div>
  <div>

    <img @click.stop="goToProfile" :src="keep.creator.picture" class=" img-fluid p-0 rounded-pill selectable" alt="" />
  </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'

import { useRouter } from 'vue-router'
import Pop from '../utils/Pop.js'


export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      async setActive() {
        try {
          await keepsService.setActive(props.keep)



          // keepsService.setActive(props.keep)

        } catch (error) {
          logger.error(error)
        }
      },
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      pkeeps: computed(() => AppState.profileKeeps),

      // akeeps: computed(() => AppState.accountKeeps),
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide()
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      },
    }
  }
}
</script>


<style lang="scss" scoped>
h3 {
  text-shadow: 4px 4px 4px black !important;

}
</style>