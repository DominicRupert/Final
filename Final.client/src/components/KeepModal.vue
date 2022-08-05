<template>
  <Modal id="keep-modal" class="keepmodal">
    <template #modal-title
      >
      <div class="d-flex justify-content-between align-items-center ">
      {{ keep.name }} by: {{ keep.creator?.name }}
      <img
        @click="goToProfile"
        :src="keep.creator?.picture"
        class="pfp selectable object-fit rounded-circle"
        alt=""
      />
      </div> 
    </template>
    <template #modal-body>
      <div class="container-fluid position-relative">
        <div class="row">
          <div class="col-md-6 d-flex flex-column">
           <div class="dropdown d-flex justify-content-center">
          <button
            class="btn btn-primary mb-3 dropdown-toggle"
            type="button"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Add to Vault
          </button>
          <ul class="dropdown-menu">
            <li v-for="v in vaults" :key="v.id">
              <a @click="addToVault(v.id)" class="dropdown-item">{{
                v.name
              }}</a>
            </li>
          </ul>
        </div>
        <div>
            <img
              :src="keep.img"
              class="w-100 h-100 object-fit keepimg"
              :alt="keep.img"
            />
          </div>
        </div>
          <div
            class="
              col-md-6
              align-items-center
              flex-column
              justify-content-center
              d-flex
            "
          >
            <h3>View Count: {{ keep.views }}</h3>
            <h3>Kept Count: {{ keep.kept }}</h3>
            <h3>Description: {{ keep.description }}</h3>

            <div>
              <button
                class="mdi mdi-delete btn text-danger btn-success"
                @click.stop="deleteVaultKeeps(keep.id)"
              >
                Remove from vault?
              </button>
            </div>
          </div>
        </div>
      </div>
  
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted, reactive, ref } from 'vue'
import { Modal } from 'bootstrap'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { vaultKeepsService } from '../services/VaultKeepsService.js'
import { useRouter } from 'vue-router'
import { useRoute } from 'vue-router'

import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
export default {
  props: {
    keep: {
      type: Object,
    },
    vaultKeep: {
      type: Object,
    }
  },


  setup(props) {
    const router = useRouter();
    const route = useRoute()
    return {

      keep: reactive(route.params.keep),

      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.profileKeeps),
      vaultKeep: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      async addToVault(id) {
        try {
          await keepsService.addToVault(AppState.activeKeep.id, id);
          Pop.toast("Keep added to vault", "success");
        }
        catch (error) {
          logger.error(error);
        }
      },
      goToProfile() {
        Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
        router.push({ name: "Profile", params: { id: AppState.activeKeep?.creatorId } });
      },
      async deleteVaultKeeps(keepId) {
        try {
          if (await Pop.confirm("Are you sure you want to remove this keep?")) {
            await vaultKeepsService.removeKeep(keepId);
            Pop.toast("Keep removed")
          }
        }
        catch (error) {
          logger.error(error)
        }

      }
    };
  },
}
</script>


<style lang="scss" scoped>
.keepmodal {
}
.pfp {
  height: 50px;
  width: 50px;
}
.keepimg {
  min-height: 50%;
  @media screen and (max-width: 768px) {
    min-height: 100%;
  }
    
  }

</style>