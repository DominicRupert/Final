<template>
  <div
    class="selectable card"
    @click.stop="setActive"
    data-bs-target="#keep-modal"
    data-bs-toggle="modal"
  >
    <img :src="keep.img" class="img-fluid card-img" alt="" />
    <div class="card-img-overlay d-flex justify-content-around align-items-end flex-row">
  
        <h3 class="card-title txt text-white justify-content-between d-flex">
          {{ keep.name }}
        </h3>
        <button
          class="mdi mdi-delete btn text-danger btn-dark d-flex align-items-end mb-2"
          v-if="keep.creator.id == account.id"
          @click.stop="deleteKeep(keep.id)"
        ></button>
      <button class="btn d-flex align-items-end">

        <img
          @click.stop="goToProfile"
          :src="keep.creator.picture"
          class="p-0 rounded-circle pfp selectable"
          alt=""
        />
      </button>
    </div>
  </div>
</template>
<script>
import { computed, onMounted } from 'vue'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger.js'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
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
          await keepsService.getKeep(props.keep.id)
        } catch (error) {
          logger.error(error)
        }
      },
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      pkeeps: computed(() => AppState.profileKeeps),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      activeKeep: computed(() => AppState.activeKeep),
      async goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).toggle()
        router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
      },
      async deleteKeep(keepId) {
        try {
          logger.log(keepId)
          if (await Pop.confirm("Are you sure you want to delete this keep?")) {
            await keepsService.deleteKeep(keepId)
            Pop.toast("Keep deleted")
          }
        }
        catch (error) {
          logger.error(error)
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
h3 {
  text-shadow: 4px 4px 4px black !important;
}
.pfp {
  height: 50px;
  width: 50px;
}
</style>