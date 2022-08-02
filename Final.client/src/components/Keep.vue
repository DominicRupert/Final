<template>
  <div
    class="selectable"
    @click="setActive"
    data-bs-target="#keep-modal"
    data-bs-toggle="modal"
  >
    <div>
      <p>{{ keep.creator.name }}</p>
    </div>
    <div class="card bg-dark">
      <h3 class="">{{ keep.name }}</h3>
    </div>
    <div>
      <img :src="keep.img" class="img-fluid" alt="" />
    </div>
    <div class="bg-dark"></div>
  </div>
  <img
    @click="goToProfile"
    :src="keep.creator.picture"
    class="pfp img-fluid p-0 rounded-pill selectable"
    alt=""
  />
</template>


<script>
import { computed, onMounted } from 'vue'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'

import { useRouter } from 'vue-router'


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
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      pkeeps: computed(() => AppState.profileKeeps),
      akeeps: computed(() => AppState.accountKeeps),
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide()
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      },

      async setActive() {
        try {
          await keepsService.setActive(props.keep)



          // keepsService.setActive(props.keep)

        } catch (error) {
          logger.error(error)
        }
      },



    }
  }
}
</script>


<style lang="scss" scoped>
.pfp {
  position: relative;
  top: -100px;
  left: 50px;
  width: 75px;
}
</style>